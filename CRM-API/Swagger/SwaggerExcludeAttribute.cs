namespace CRM_API.Swagger
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class SwaggerExcludeAttribute : Attribute
    {
        public SwaggerExcludeAttribute()
        {

        }
    }
}
