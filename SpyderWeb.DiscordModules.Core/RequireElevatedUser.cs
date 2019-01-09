using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace SpyderWeb.DiscordModules.Core
{
    public class RequireElevatedUser : PreconditionAttribute
    {
        private static readonly Task<PreconditionResult> NotUser = Task.FromResult(PreconditionResult.FromError("This command may only be ran in a guild."));
        private static readonly Task<PreconditionResult> NotElevated = Task.FromResult(PreconditionResult.FromError("You are not elevated in this guild."));
        private static readonly Task<PreconditionResult> Elevated = Task.FromResult(PreconditionResult.FromSuccess());
        private readonly List<ulong> _userIds;
        public RequireElevatedUser(List<ulong> userIds = null)
        {
            _userIds = userIds ?? new List<ulong>();
        }
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (!(context.User is SocketGuildUser user)) return NotUser;
            if (IsElevated(context.User as SocketGuildUser)) return Elevated;
            return NotElevated;
        }

        private bool IsElevated(IUser user) => _userIds.Contains(user.Id);
    }
}