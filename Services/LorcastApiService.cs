using Newtonsoft.Json;

namespace Companion.Console.Services;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class LorcastApiService : IDisposable
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://lorcast.com/v0";

    public LorcastApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<CardDto> GetCardByIdAsync(string cardId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/cards/{cardId}");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CardDto>(jsonResponse);
    }

    public async Task<IEnumerable<CardDto>?> SearchCardsAsync(string query)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/cards/search?q={query}");
        if (!response.IsSuccessStatusCode) return null;
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var searchResult = JsonConvert.DeserializeObject<CardSearchResultDto>(jsonResponse);
        return searchResult.results;
    }
    
    public async Task<IEnumerable<CardDto>?> SearchCardsByNameAndVersionAsync(string name, string version)
    {
        var query = $"{name} version:{version}";
        return await SearchCardsAsync(query);
    }
    
    public async Task<IEnumerable<CardDto>?> SearchCardsByNameAndVersionAsync(string name, string version, bool enchanted)
    {
        var query = $"{name} version:{version}+rarity:enchanted";
        return await SearchCardsAsync(query);
    }
    public async Task<IEnumerable<CardDto>?> SearchCardsByNameAsync(string name)
    {
        var query = $"{name}";
        return await SearchCardsAsync(query);
    }

    public async Task<IEnumerable<SetDto>> GetSetsAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/sets");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<SetDto>>(jsonResponse);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public class CardDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        // Add other properties as needed
    }
    
    public class SetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TotalCards { get; set; }
        // Add other properties as needed based on the API documentation
    }
    
    public class CardSearchResultDto
    {
        public IEnumerable<CardDto>? results { get; set; }
    }


}