using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Mapper.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class UserCredentialsService : IUserCredentialsService
    {
        #region Injection

        private readonly ILogger<UserCredentialsService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserCredentialsRepository _userCredentialsRepository;

        public UserCredentialsService(ILogger<UserCredentialsService> logger, IMapper mapper, IUserCredentialsRepository userCredentialsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userCredentialsRepository = userCredentialsRepository;
        }

        #endregion Injection

        public IEnumerable<UserCredentialsDTO> GetUserCredentials()
        {
            var userCredentials = _userCredentialsRepository.GetAll();
            return _mapper.Map<IEnumerable<UserCredentialsDTO>>(userCredentials);
        }

        public UserCredentialsDTO GetUserCredential(int userCredentialsID)
        {
            var userCredentials = _userCredentialsRepository.GetByID(userCredentialsID);
            return _mapper.Map<UserCredentialsDTO>(userCredentials);
        }

        public void AddUserCredential(UserCredentialsDTO userCredentialsDTO)
        {
            _userCredentialsRepository.Insert(_mapper.Map<UserCredentials>(userCredentialsDTO));
            _userCredentialsRepository.Save();
        }

        public void UpdateUserCredential(int userCredentialsID, UserCredentialsDTO userCredentialsDTO)
        {
            _userCredentialsRepository.Update(userCredentialsID, _mapper.Map<UserCredentials>(userCredentialsDTO));
            _userCredentialsRepository.Save();
        }

        public void DeleteUserCredential(int userCredentialsID)
        {
            _userCredentialsRepository.Delete(userCredentialsID);
            _userCredentialsRepository.Save();
        }
    }
}
