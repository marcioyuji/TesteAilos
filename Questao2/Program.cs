using Newtonsoft.Json;
using Questao2;
using System.Net;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                int resultado = 0;
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<ResultadoJogo>(await response.Content.ReadAsStringAsync()).data.Sum(x => x.team1goals);
                }

                return resultado;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Ocorreu um Erro!");
                Console.WriteLine("Mensagem: {0} ", e.Message);
            }
        }
        return 0;
    }

}