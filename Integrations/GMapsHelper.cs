public static class GMapsHelper
{
    public const double auxPi = 0.0174532925199433;

    public static dynamic GetLatitudeLongitudeLocationFromCEP(string targetUrl, string apiKey, string cep)
    {
        dynamic retorno;
        using (var client = new HttpClient())
        {
            var response = client.GetAsync(string.Format("{0}?address={1}&key={2}", targetUrl, cep, apiKey)).Result;
            retorno = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
        }
        var latitudeLongitude = retorno.results?.First.geometry?.location;

        return latitudeLongitude;
    }

    public static dynamic GetLocationFromAddress(string targetUrl, string apiKey, string address)
    {
        dynamic retorno;
        address = address.Replace(" ", "+");
        using (var client = new HttpClient())
        {
            var response = client.GetAsync(string.Format("{0}?address={1}&key={2}", targetUrl, address, apiKey)).Result;
            retorno = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
        }
        var addressResult = retorno.results?.First;

        return addressResult;
    }

    public static Double DistanceBetweenLatAndLong(double latIni, double latFim, double lonIni, double lonFim)
    {
        return Math.Round((40030 * ((180 / Math.PI) *
        Math.Acos(Math.Cos((90 - latFim) * auxPi) *
        Math.Cos((90 - latIni) * auxPi) +
        Math.Sin((90 - latFim) * auxPi) *
        Math.Sin((90 - latIni) * auxPi) *
        Math.Cos((lonFim - lonIni) * auxPi)))) / 360, 2);
    }
}