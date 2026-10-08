using Domain.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Worker.Services
{
    public class JobApiClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public JobApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> RegisterWorkerAsync(
            CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                "Worker/add",
                null,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            Domain.Entities.Worker? worker = await response.Content
                .ReadFromJsonAsync<Domain.Entities.Worker>(
                    JsonOptions,
                    cancellationToken);

            if (worker == null)
                throw new InvalidOperationException(
                    "Worker registration returned no data.");

            return worker.Id;
        }

        public async Task<Job?> RequestJobAsync(
            Guid workerId,
            CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                $"JobAssignment/{workerId}/jobs/request",
                null,
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NoContent)
                return null;

            response.EnsureSuccessStatusCode();

            Job? job = await response.Content
                .ReadFromJsonAsync<Job>(
                    JsonOptions,
                    cancellationToken);

            return job;
        }

        public async Task StartJobAsync(
            Guid workerId,
            Guid jobId,
            CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                $"Job/{jobId}/start?workerId={workerId}",
                null,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task CompleteJobAsync(
            Guid workerId,
            Guid jobId,
            CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                $"Job/{jobId}/complete?workerId={workerId}",
                null,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task FailedJobAsync(
            Guid workerId,
            Guid jobId,
            CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                $"Job/{jobId}/failed?workerId={workerId}",
                null,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task ShutdownWorkerAsync(
    Guid workerId,
    CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(
                $"Worker/{workerId}/shutdown",
                null,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }
    }
}