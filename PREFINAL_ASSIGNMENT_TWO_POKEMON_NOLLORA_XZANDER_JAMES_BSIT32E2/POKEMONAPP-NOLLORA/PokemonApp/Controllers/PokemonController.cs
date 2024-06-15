using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class PokemonController : Controller
{
    private readonly HttpClient _httpClient;

    public PokemonController()
    {
        _httpClient = new HttpClient();
    }

    public IActionResult Index()
    {
        var defaultPokemon = new Pokemon
        {
            Name = "",
            Abilities = new List<Pokemon.PokemonAbility>(),
            Moves = new List<Pokemon.PokemonMove>()
        };
        return View(defaultPokemon);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return View("Index", new Pokemon()); // Return default view with empty Pokemon
        }

        try
        {
            var pokemon = await GetPokemon(name.ToLower());
            return View(pokemon);
        }
        catch (HttpRequestException)
        {
            ViewData["Error"] = "Pokemon not found.";
            return View("Index", new Pokemon());
        }
    }

    private async Task<Pokemon> GetPokemon(string name)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            dynamic jsonResponse = JsonConvert.DeserializeObject(response);

            var pokemon = new Pokemon
            {
                Name = jsonResponse.name,
                SpriteUrl = jsonResponse.sprites.front_default,
                Abilities = jsonResponse.abilities != null
                    ? jsonResponse.abilities.ToObject<List<Pokemon.PokemonAbility>>()
                    : new List<Pokemon.PokemonAbility>(),
                Moves = jsonResponse.moves != null
                    ? jsonResponse.moves.ToObject<List<Pokemon.PokemonMove>>()
                    : new List<Pokemon.PokemonMove>()
            };

            return pokemon;
        }
        catch (HttpRequestException)
        {
            ViewData["Error"] = "Pokemon not found.";
            return new Pokemon(); // Return an empty Pokemon object
        }
    }
}
