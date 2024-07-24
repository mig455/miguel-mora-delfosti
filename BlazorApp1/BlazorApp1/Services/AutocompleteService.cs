namespace BlazorApp1.Services
{
    public class AutocompleteService
    {
        public Task<List<string>> GetSuggestionsAsync(string query,List<string> Data)
        {
            var suggestions = Data.Where(item => item.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            return Task.FromResult(suggestions);
        }
    }
}
