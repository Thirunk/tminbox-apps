using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.DTOs
{
    public record ServiceResult(ServiceResultType Type, string? Message = null)
    {
        public bool IsBadRequest => Type == ServiceResultType.BadRequest;
        public bool IsNotFound => Type == ServiceResultType.NotFound;
        public bool IsOk => Type == ServiceResultType.Ok;
        public bool IsError => Type == ServiceResultType.Error;

    }

    public record ServiceResult<T> : ServiceResult
    {
        public T? Value { get; set; }
        public ServiceResult(T? Value, ServiceResultType Type=ServiceResultType.Ok, string? Message = null):base(ServiceResultType.Ok, Message) {
        
            this.Value = Value;
            this.Type = Type;
            this.Message = Message;
        }

        public void Deconstruct(out T? Value, out ServiceResultType Type , out string? Message) {
            Value=this.Value;
            Type = this.Type;
            Message=this.Message;
        }
    }
    
}
