using Domain.Entities;

namespace Worker.Handlers
{
    public class CheckImageExists : IJobHandler
    {
        public string JobType => "CheckImageExists";

        public async Task ExecuteAsync(Job job)
        {
            string path = job.Payload;

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(
                    "Path cannot be null or empty.",
                    nameof(path));
            }

            await Task.Delay(10000);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"The file at path '{path}' does not exist.",
                    path);
            }
        }
    }
}