namespace NetDevLive01.Web.Exceptions;

public class SectionNotFoundException : Exception
{
    public string FieldName { get; }

    public SectionNotFoundException(string fieldName, string? message = null, Exception? inner = null) 
        : base(message, inner)
    {
        FieldName = fieldName;
    }
}