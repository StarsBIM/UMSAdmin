namespace UMS.Common
{
    /// <summary>
    /// Http响应模型
    /// </summary>
    public class ResultModel<T>
    {
        /// <summary>
        /// 是否响应成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; } = "";
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 成功啦
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="errMsg">error message</param>
        /// <returns></returns>
        public static ResultModel<T> Success(T data, string errMsg = "")
        {
            if (data == null)
            {
                return Error("出现了一些错误");
            }
            return new ResultModel<T> { Data = data, ErrorMessage = errMsg, IsSuccess = true, Code = 200 };
        }
        /// <summary>
        /// 出错啦
        /// </summary>
        /// <param name="str">error message</param>
        /// <param name="code">status code</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static ResultModel<T> Error(string str, int code = 400, T data = default)
        {
            return new ResultModel<T> { Data = data, ErrorMessage = str, IsSuccess = false, Code = code };
        }
    }
}
