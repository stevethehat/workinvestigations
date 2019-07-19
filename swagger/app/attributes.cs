using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerTests
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute{
        public string Info { get; private set; }
        public PropertyAttribute(string info){
            Info = info;
        }
    }

    public class Test : ISchemaFilter
    {
        void ISchemaFilter.Apply(Schema schema, SchemaFilterContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class DataAnnotationSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext schemaFilterContext)
        {
            var type = schemaFilterContext.SystemType;

            var propertyMappings = type
                .GetProperties()
                .Join(
                    schema.Properties ?? new Dictionary<string, Schema>(),
                    x => x.Name.ToLower(),
                    x => x.Key.ToLower(),
                    (x, y) => new KeyValuePair<PropertyInfo, KeyValuePair<string, Schema>>(x, y))
                .ToList();

            foreach (var propertyMapping in propertyMappings)
            {
                var propertyInfo = propertyMapping.Key;
                var propertyNameToSchemaKvp = propertyMapping.Value;

                foreach (var attribute in propertyInfo.GetCustomAttributes())
                {
                    //schema.Description = "what does this change";
                    SetSchemaDetails(schema, propertyNameToSchemaKvp, propertyInfo, attribute);
                }
            }
        }

        private static void SetSchemaDetails(Schema parentSchema, KeyValuePair<string, Schema> propertyNameToSchemaKvp, PropertyInfo propertyInfo, object propertyAttribute)
        {
            var schema = propertyNameToSchemaKvp.Value;

            if (propertyAttribute is DataTypeAttribute)
            {
                var dataType = ((DataTypeAttribute)propertyAttribute).DataType;
                schema.Format = ((DataTypeAttribute)propertyAttribute).CustomDataType;
                /*
                if (dataType == DataType.Date)
                {
                    schema.Format = "date";
                    schema.Type = "date";
                }
                */
            }

            /*
            if (propertyAttribute is ReadOnlyAttribute)
            {
                schema.ReadOnly = ((ReadOnlyAttribute)propertyAttribute).IsReadOnly;
            }
            */
        }
    }

    /*
    public class ApplyPropertyAttribute: IOperationFilter{

    }
    */
}   