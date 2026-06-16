namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorInputShareCodec
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public static string Encode(CalculatorInput input)
    {
        var json = JsonSerializer.Serialize(input, SerializerOptions);
        return Base64UrlEncode(Encoding.UTF8.GetBytes(json));
    }

    public static bool TryDecode(string? token, out CalculatorInput input)
    {
        input = default!;
        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        try
        {
            var json = Encoding.UTF8.GetString(Base64UrlDecode(token));
            var decoded = JsonSerializer.Deserialize<CalculatorInput>(json, SerializerOptions);
            if (decoded is null)
            {
                return false;
            }

            input = decoded;
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static string Base64UrlEncode(byte[] data) =>
        Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private static byte[] Base64UrlDecode(string token)
    {
        var base64 = token.Replace('-', '+').Replace('_', '/');
        var padding = (4 - base64.Length % 4) % 4;
        base64 += new string('=', padding);
        return Convert.FromBase64String(base64);
    }
}
