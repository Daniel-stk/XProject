namespace Responses.Tools
{
    public static class ResponseExtensions
    {
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
