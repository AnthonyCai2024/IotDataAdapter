﻿namespace Mtim.ProtoHub.WebApp.Protocol;

public interface IProtocolStrategy
{
    void ExecuteCommand(string deviceAddress, string command);
}