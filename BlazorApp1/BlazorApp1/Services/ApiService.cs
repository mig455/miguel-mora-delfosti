using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> Login(User user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", user);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AuthResponse>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AuthResponse> Register(User user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/register", user);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AuthResponse>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Project>>("Project");

            }
            catch (Exception ex) 
            {
                return new List<Project>();
            }
        }

        public async Task<UserDto> GetUserById()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDto>("User/GetUserById");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<Tarea[]> GetTasks(int projectId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Tarea[]>($"Tarea/GetTaskByProject/{projectId}");

            }
            catch (Exception ex) 
            {
                return new Tarea[0];
            }
        }

        public async Task<bool> CreateTask(TareaSaveDto task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Tarea", task);
                response.EnsureSuccessStatusCode();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTask(TareaUpdateDto task,int Id)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Tarea/{Id}", task);
                response.EnsureSuccessStatusCode();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
