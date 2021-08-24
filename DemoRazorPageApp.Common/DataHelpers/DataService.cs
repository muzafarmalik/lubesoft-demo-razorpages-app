using DemoRazorPageApp.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;



namespace DemoRazorPageApp.Common.DataHelpers
{
    public static class DataService
    {
        #region Generic Response

        public static BaseResponse Response(string error, object data = null)
        {
            return new BaseResponse
            {
                Success = error == null ? true : false,
                Error = error,
                Data = data
            };
        }

        #endregion

        #region Error Handling

        public static string Errors(ModelStateDictionary modelState)
        {
            if (modelState == null)
            {
                return null;
            }

            string message = string.Join(" ", modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return message;
        }

        #endregion

        #region DataTable request

        public static DataTableRequest TransformIntoModel(HttpRequest request)
        {
            var draw = GetFormValue("draw", request);
            var start = GetFormValue("start", request);
            var length = GetFormValue("length", request);
            var sortColumn = GetFormValue("columns[" + GetFormValue("order[0][column]", request) + "][name]", request);
            var sortColumnDir = GetFormValue("order[0][dir]", request);
            var searchValue = GetFormValue("search[value]", request);
            searchValue = searchValue?.ToLower().Trim();

            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            return new DataTableRequest
            { 
                Draw = draw,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchString = searchValue,
                PageSize = pageSize,
                Skip = skip
            };
        }

        private static string GetFormValue(string key, HttpRequest request)
        {
            bool hasValue = request.Form.TryGetValue(key, out StringValues draw);
            return hasValue ? draw.FirstOrDefault() : null;
        }

        #endregion
    }
}
