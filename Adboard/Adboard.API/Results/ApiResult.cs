namespace AdvertPlatform.WebApi.Results
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public sealed class ApiResult : IActionResult
    {
        private readonly object _data;

        public ApiResult(object data) => _data = data;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;

            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "application/json";

            if (_data != null)
            {
                var apiResponse = new ApiResponse<object>(_data);
                var json = JsonConvert.SerializeObject(apiResponse);

                await response.WriteAsync(json);
            }
        }
    }

    public sealed class ApiResponse
    {
        public bool HasErrors => Errors.Any();

        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse() { }

        public ApiResponse(string error) => Errors.Add(error);
    }

    public sealed class ApiResponse<TData>
    {
        public bool HasErrors => Errors.Any();

        public TData Data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse() { }

        public ApiResponse(TData data) => Data = data;
    }
}