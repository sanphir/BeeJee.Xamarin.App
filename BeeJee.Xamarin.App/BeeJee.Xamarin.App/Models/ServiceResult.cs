using BeeJee.Xamarin.App.Enums;
using System.Collections.Generic;

namespace BeeJee.Xamarin.App.Models
{
    public class ServiceResult<TData> where TData : class
    {
        public ResultStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public TData Data { get; set; }
    }
}
