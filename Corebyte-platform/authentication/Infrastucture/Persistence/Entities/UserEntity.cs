﻿namespace Corebyte_platform.authentication.Infrastucture.Persistence.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}