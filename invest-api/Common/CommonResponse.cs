using invest_api.Enum;

namespace invest_api.Common
{
  public class CommonResponse
  {
     public ECommonStatus Status { get; set; }
     public string Message { get; set; }

    public object Data { get; set; }

    public CommonResponse(ECommonStatus status, string message = "") {
      Status = status;
      Message = message;
      Data = null;
    }

    public CommonResponse(Enum.ECommonStatus status, string message, object data)
    {
      Status = status;
      Message = message;
      Data = data;
    }

  }
}
