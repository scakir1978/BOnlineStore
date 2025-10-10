using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Yeni kullanýcý oluþturur
        /// </summary>
        /// <param name="userCreateDto">Oluþturulacak kullanýcý bilgileri</param>
        /// <returns>Oluþturulan kullanýcý bilgileri</returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (user, result) = await _userService.CreateAsync(userCreateDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Kullanýcý bilgilerini günceller
        /// </summary>
        /// <param name="userUpdateDto">Güncellenecek kullanýcý bilgileri</param>
        /// <returns>Güncellenmiþ kullanýcý bilgileri</returns>
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (user, result) = await _userService.UpdateAsync(userUpdateDto);

            if (result.Succeeded)
            {
                return Ok(user);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Kullanýcýyý ID ile getirir
        /// </summary>
        /// <param name="id">Kullanýcý kimliði</param>
        /// <returns>Kullanýcý bilgileri</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"ID'si {id} olan kullanýcý bulunamadý.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Kullanýcýyý email ile getirir
        /// </summary>
        /// <param name="email">Email adresi</param>
        /// <returns>Kullanýcý bilgileri</returns>
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"Email adresi {email} olan kullanýcý bulunamadý.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Kullanýcýyý siler
        /// </summary>
        /// <param name="id">Silinecek kullanýcý kimliði</param>
        /// <returns>Ýþlem sonucu</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Belirli bir kiracýya ait kullanýcýlarý getirir
        /// </summary>
        /// <param name="tenantId">Kiracý kimliði</param>
        /// <returns>Kullanýcý listesi</returns>
        [HttpGet("by-tenant/{tenantId:guid}")]
        public async Task<ActionResult<List<UserDto>>> GetUsersByTenantId(Guid tenantId)
        {
            var users = await _userService.GetUsersByTenantIdAsync(tenantId);
            return Ok(users);
        }

        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        /// <returns>Kullanýcý listesi</returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Kullanýcýnýn þifresini deðiþtirir
        /// </summary>
        /// <param name="request">Þifre deðiþtirme isteði</param>
        /// <returns>Ýþlem sonucu</returns>
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Þifre baþarýyla deðiþtirildi." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Kullanýcýnýn þifresini sýfýrlar (admin iþlemi)
        /// </summary>
        /// <param name="request">Þifre sýfýrlama isteði</param>
        /// <returns>Ýþlem sonucu</returns>
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ResetPasswordAsync(request.UserId, request.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Þifre baþarýyla sýfýrlandý." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }

    /// <summary>
    /// Þifre deðiþtirme isteði
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Mevcut þifre
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Yeni þifre
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// Þifre sýfýrlama isteði
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Yeni þifre
        /// </summary>
        public string NewPassword { get; set; }
    }
}