using Vision.Datas;
using Vision.Exceptions;
using IDataObject = Vision.Interfaces.IDataObject;
using System.Net;

namespace TestRiaApiEvaluationModel
{
    /// <summary>
    /// This test class is designed to confirm the AwsBucketManager class's behavior
    /// </summary>
    [TestClass]
    public class TestGoogleBucketManagerImpl
    {
        IDataObject dataObject = new GoogleDataObjectImpl();
        static string bucketUri = Environment.GetEnvironmentVariable("BucketName");
        static string localFileName = Environment.GetEnvironmentVariable("Image");
        string localFile = Environment.GetEnvironmentVariable("Image");
        string objectUriWithSubFolder = "";
        string objectUri = localFileName;
        [TestInitialize()]
        public void Startup()
        {
            dataObject = new GoogleDataObjectImpl();
            if (!File.Exists(localFile))
            {
                File.Copy(localFile + ".copy", localFile, true);
            }
            objectUri = localFileName;
            bucketUri = Environment.GetEnvironmentVariable("BucketName");
        }

        [TestCleanup]
        public async Task CleanUp()
        {
            await this.dataObject.Remove(objectUri);
        }

        [TestMethod]
        public async Task DoesExist_ExistingBucket_BucketExists()
        {
            //given
            //The bucket is always available

            //when

            //then
            Assert.IsTrue(await this.dataObject.DoesExists(bucketUri));
        }

        [TestMethod]
        public async Task DoesExist_ExistingObject_ObjectExists()
        {
            //given
            //The bucket is always available
            await this.dataObject.Upload(localFile, objectUri);

            //when
            //check the assertion

            //then
            Assert.IsTrue(await this.dataObject.DoesExists(objectUri));
        }

        [TestMethod]
        public async Task DoesExist_MissingObject_ObjectNotExists()
        {
            //given
            //The bucket is always available
            //The bucket is empty (or does not contain the expected object)

            //when
            //check the assertion

            //then
            Assert.IsFalse(await this.dataObject.DoesExists(objectUri));
        }

        [TestMethod]
        public async Task Upload_BucketAndLocalFileAreAvailable_NewObjectCreatedOnBucket()
        {
            //given
            Assert.IsTrue(await this.dataObject.DoesExists(bucketUri));
            Assert.IsFalse(await this.dataObject.DoesExists(objectUri));

            //when
            await this.dataObject.Upload(localFile, objectUri);

            //then
            Assert.IsTrue(await this.dataObject.DoesExists(objectUri));
        }
        [TestMethod]
        public async Task Publish_ObjectExists_PublicUrlCreated()
        {
            //given
            await this.dataObject.Upload(localFile, objectUri);

            Assert.IsTrue(await this.dataObject.DoesExists(bucketUri));
            Assert.IsTrue(File.Exists(localFile));

            //when
            string presignedUrl = await this.dataObject.Publish(objectUri);
            //TODO download file using wget or another method that do not need our project library

            WebClient webClient = new WebClient();
            webClient.DownloadFile(presignedUrl, localFile);
            //then
            Assert.IsTrue(File.Exists(localFile));
        }
        [TestMethod]
        public async Task Publish_ObjectMissing_ThrowException()
        {
            //given
            Assert.IsFalse(await this.dataObject.DoesExists(objectUri));

            //when
            await Assert.ThrowsExceptionAsync<ObjectNotFoundException>(async () => await this.dataObject.Publish(objectUri));

            //then
            //Exception thrown
        }
        [TestMethod]
        public async Task Download_ObjectAndLocalPathAvailable_ObjectDownloaded()
        {
            //given
            await this.dataObject.Upload(localFile, objectUri);
            if (File.Exists(localFile))
            {
                File.Copy(localFile, localFile + ".copy", true);
                File.Delete(localFile);
            }
            Assert.IsTrue(await this.dataObject.DoesExists(objectUri));
            Assert.IsFalse(File.Exists(localFile));

            //when
            await this.dataObject.Download(objectUri, localFile);

            //then
            Assert.IsTrue(File.Exists(localFile));
        }

        [TestMethod]
        public async Task Download_ObjectMissing_ThrowException()
        {
            //given
            File.Delete(localFile);
            objectUri = "noWaffles.png";
            Assert.IsFalse(await this.dataObject.DoesExists(objectUri));
            Assert.IsFalse(File.Exists(localFile));

            //when
            await Assert.ThrowsExceptionAsync<ObjectNotFoundException>(async () => await this.dataObject.Download(objectUri, ""));

            //then
            //Exception throwns
        }

        [TestMethod]
        public async Task Remove_ObjectPresentNoFolder_ObjectRemoved()
        {
            //given
            await this.dataObject.Upload(localFile, objectUri);
            bucketUri = bucketUri + "/" + objectUri;
            Assert.IsTrue(await this.dataObject.DoesExists(objectUri));

            //when
            await this.dataObject.Remove(bucketUri);

            //then
            Assert.IsFalse(await this.dataObject.DoesExists(bucketUri));
        }

        [TestMethod]
        public async Task Remove_ObjectAndFolderPresent_ObjectRemoved()
        {
            //given
            //The bucket contains object at the root level as well as in subfolders
            //Sample: mybucket.com/myobject     //myObject is a folder
            //        mybucket.com/myobject/myObjectInSubfolder
            objectUriWithSubFolder = objectUri + "/hi";
            await this.dataObject.Upload(localFile, objectUriWithSubFolder);
            await this.dataObject.Upload(localFile, objectUri);
            bucketUri = bucketUri + "/" + objectUri;
            Assert.IsTrue(await this.dataObject.DoesExists(objectUri));
            Assert.IsTrue(await this.dataObject.DoesExists(objectUriWithSubFolder));

            //when
            await this.dataObject.Remove(bucketUri, true);

            //then
            Assert.IsFalse(await this.dataObject.DoesExists(objectUri));
        }
    }
}
