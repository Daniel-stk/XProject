namespace Responses.Tools
{
    public static class ResponseExtensions
    {
        public enum ResponseType { simple, complex };
        public static ComplexResponse<T> ConvertToComplexResponse<T>(this Response response)
        {
            return response as ComplexResponse<T>;
        }

        public static SimpleResponse<T> ConvertToSimpleResponse<T>(this Response response)
        {
            return response as SimpleResponse<T>;
        }
    }
}
