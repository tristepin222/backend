namespace Vision.Exceptions
{

    public class GoogleDataObjectImplException : Exception { }

    public class ObjectAlreadyExistsException : GoogleDataObjectImplException { }
    public class ObjectNotFoundException : GoogleDataObjectImplException { }
    public class NotEmptyObjectException : GoogleDataObjectImplException { }
}
