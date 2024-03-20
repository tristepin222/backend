namespace Vision.Exceptions
{

    public class DataObjectImplException : Exception { }

    public class ObjectAlreadyExistsException : DataObjectImplException { }
    public class ObjectNotFoundException : DataObjectImplException { }
    public class NotEmptyObjectException : DataObjectImplException { }
}
