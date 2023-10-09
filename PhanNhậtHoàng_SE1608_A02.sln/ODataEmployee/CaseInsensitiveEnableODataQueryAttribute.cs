//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.OData.Query.Validator;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.AspNetCore.OData.Routing.Conventions;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.AspNetCore.OData.Query.Validator;
//using Microsoft.OData;
//using Microsoft.OData.UriParser;
//namespace ODataEmployee
//{
//    public class CaseInsensitiveEnableODataQueryAttribute : EnableQueryAttribute
//    {
//        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
//        {
//            base.ValidateQuery(request, queryOptions);

//            foreach (var validator in queryOptions.ValidationSettings.ModelBoundQuerySettings.Validators)
//            {
//                if (validator is FilterQueryValidator filterValidator)
//                {
//                    filterValidator.AllowedLogicalOperators = AllowedLogicalOperators.All;
//                    filterValidator.AllowedFunctions = AllowedFunctions.AllFunctions;
//                    filterValidator.AllowedArithmeticOperators = AllowedArithmeticOperators.All;
//                    filterValidator.AllowedStringFunctions = AllowedFunctions.AllStringFunctions;
//                    filterValidator.AllowedMathFunctions = AllowedFunctions.AllMathFunctions;
//                    filterValidator.AllowedDatetimeFunctions = AllowedFunctions.AllDateTimeFunctions;
//                    filterValidator.AllowedNavigationProperties = AllowedNavigationProperties.All;
//                    filterValidator.AllowedQueryOptions = AllowedQueryOptions.All;
//                }
//            }
//        }
//    }
//}