# ODataPlayground

A minimal .NET 10 OData API for exploring query options, batch requests, and API behavior.

The `/odata/DailyForecasts` endpoint returns an in-memory forecast with `Id = 1` and `Temp = 25`.

## Run

```bash
dotnet run
```

The HTTP launch profile listens at `http://localhost:5172`.

## Filter a request

Use `$filter` to select forecasts by a property:

```http
GET http://localhost:5172/odata/DailyForecasts?$filter=Temp%20gt%2020 HTTP/1.1
Accept: application/json
```

This returns the sample forecast because its `Temp` is 25. The endpoint also enables `$select`, `$orderby`, `$expand`, `$count`, and `$top` (up to 100).

## Send a batch request

Send OData operations to `/odata/$batch` as a multipart request. This minimal batch contains one filtered GET:

```http
POST http://localhost:5172/odata/$batch  HTTP/1.1
Content-Type: multipart/mixed; boundary=batch_123

--batch_123
Content-Type: application/http
Content-Transfer-Encoding: binary

GET /odata/DailyForecasts?$filter=Temp%20gt%2020 HTTP/1.1
Accept: application/json

--batch_123--