﻿namespace FloorPlanner.Transfer.AuthenticationResponse;

public class RegisterResponse
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}
