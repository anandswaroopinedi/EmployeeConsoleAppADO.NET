﻿using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interface;
using Models;

namespace BusinessLogicLayer.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IDataOperations _dataOperations;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Role.json";
        public RoleManager(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }

        public async Task<bool> AddRole(Roles role)
        {
            List<Roles> roleList = GetAll().Result;
            role.Id = roleList.Count + 1;
            roleList.Add(role);
            if (await _dataOperations.AddRoleToDb(role))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CheckRoleExists(string roleName)
        {
            List<Roles> roleList = await GetAll();
            for (int i = 0; i < roleList.Count; i++)
            {
                if (roleList[i].Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<List<Roles>> GetAll()
        {
            return await _dataOperations.GetRoles();
        }
        public async Task<string> GetRoleName(int id)
        {
            List<Roles> rolesList = await GetAll();
            for (int i = 0; i < rolesList.Count; i++)
            {
                if (rolesList[i].Id == id)
                {
                    return rolesList[i].Name;
                }
            }
            return "None";
        }
    }
}
