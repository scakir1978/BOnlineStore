using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Extensions
{
    /// <summary>
    /// Mapping iþlemleri için extension methodlar
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// UserCreateDto'yu ApplicationUser'a dönüþtürür
        /// </summary>
        /// <param name="userCreateDto">Dönüþtürülecek DTO</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <returns>ApplicationUser entity</returns>
        public static ApplicationUser ToEntity(this UserCreateDto userCreateDto, IMapper mapper)
        {
            return mapper.Map<ApplicationUser>(userCreateDto);
        }

        /// <summary>
        /// ApplicationUser'ý UserDto'ya dönüþtürür
        /// </summary>
        /// <param name="user">Dönüþtürülecek entity</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <returns>UserDto</returns>
        public static UserDto ToDto(this ApplicationUser user, IMapper mapper)
        {
            return mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// ApplicationUser listesini UserDto listesine dönüþtürür
        /// </summary>
        /// <param name="users">Dönüþtürülecek entity listesi</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <returns>UserDto listesi</returns>
        public static List<UserDto> ToDto(this List<ApplicationUser> users, IMapper mapper)
        {
            return mapper.Map<List<UserDto>>(users);
        }

        /// <summary>
        /// UserUpdateDto'daki deðerleri mevcut ApplicationUser'a uygular
        /// </summary>
        /// <param name="user">Güncellenecek entity</param>
        /// <param name="updateDto">Güncelleme bilgileri</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <returns>Güncellenmiþ ApplicationUser</returns>
        public static ApplicationUser ApplyUpdate(this ApplicationUser user, UserUpdateDto updateDto, IMapper mapper)
        {
            mapper.Map(updateDto, user);
            return user;
        }

        /// <summary>
        /// Null deðerleri kontrol ederek güvenli mapping yapar
        /// </summary>
        /// <param name="source">Kaynak nesne</param>
        /// <param name="destination">Hedef nesne</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <typeparam name="TSource">Kaynak tip</typeparam>
        /// <typeparam name="TDestination">Hedef tip</typeparam>
        /// <returns>Mapping sonucu</returns>
        public static TDestination SafeMap<TSource, TDestination>(this TSource source, IMapper mapper)
            where TSource : class
            where TDestination : class
        {
            return source == null ? null : mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Partial update için sadece dolu alanlarý map eder
        /// </summary>
        /// <param name="updateDto">Güncelleme DTO'su</param>
        /// <param name="existingEntity">Mevcut entity</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <returns>Güncellenmiþ entity</returns>
        public static ApplicationUser PartialUpdate(this UserUpdateDto updateDto, ApplicationUser existingEntity, IMapper mapper)
        {
            if (updateDto == null || existingEntity == null)
                return existingEntity;

            // Sadece null olmayan deðerleri map et
            if (!string.IsNullOrEmpty(updateDto.Email))
            {
                existingEntity.Email = updateDto.Email;
                existingEntity.UserName = updateDto.Email;
            }

            if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
                existingEntity.PhoneNumber = updateDto.PhoneNumber;

            if (!string.IsNullOrEmpty(updateDto.Locale))
                existingEntity.Locale = updateDto.Locale;

            if (!string.IsNullOrEmpty(updateDto.Name))
                existingEntity.Name = updateDto.Name;

            if (!string.IsNullOrEmpty(updateDto.FamilyName))
                existingEntity.FamilyName = updateDto.FamilyName;

            if (!string.IsNullOrEmpty(updateDto.GivenName))
                existingEntity.GivenName = updateDto.GivenName;

            if (!string.IsNullOrEmpty(updateDto.MiddleName))
                existingEntity.MiddleName = updateDto.MiddleName;

            if (!string.IsNullOrEmpty(updateDto.Nickname))
                existingEntity.Nickname = updateDto.Nickname;

            if (!string.IsNullOrEmpty(updateDto.PreferredUsername))
                existingEntity.PreferredUsername = updateDto.PreferredUsername;

            if (!string.IsNullOrEmpty(updateDto.Profile))
                existingEntity.Profile = updateDto.Profile;

            if (!string.IsNullOrEmpty(updateDto.Picture))
                existingEntity.Picture = updateDto.Picture;

            if (!string.IsNullOrEmpty(updateDto.Website))
                existingEntity.Website = updateDto.Website;

            if (!string.IsNullOrEmpty(updateDto.Gender))
                existingEntity.Gender = updateDto.Gender;

            if (updateDto.Birthdate.HasValue)
                existingEntity.Birthdate = updateDto.Birthdate;

            if (!string.IsNullOrEmpty(updateDto.ZoneInfo))
                existingEntity.ZoneInfo = updateDto.ZoneInfo;

            existingEntity.UpdatedAt = DateTime.UtcNow;

            return existingEntity;
        }
    }
}