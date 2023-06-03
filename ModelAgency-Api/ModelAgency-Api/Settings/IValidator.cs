namespace ModelAgency_Api.Settings
{
    public interface IValidator<in T> where T : class
    {
        public bool IsValid();
    }
}
