namespace WebApiWithJwtSwagger.Dto
{
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public short Status { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceName { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalInfo { get; set; }

        public T Data { get; set; }
    }
    public class ResultDto:ResultDto<string>
    {

    }
}
