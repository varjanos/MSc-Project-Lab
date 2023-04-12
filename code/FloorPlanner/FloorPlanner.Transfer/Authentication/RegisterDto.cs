﻿namespace FloorPlanner.Transfer.Authentication;

public class RegisterDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordAgain { get; set; } = null!;
}
