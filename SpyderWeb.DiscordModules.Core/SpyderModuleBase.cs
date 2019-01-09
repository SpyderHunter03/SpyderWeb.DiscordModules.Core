using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace SpyderWeb.DiscordModules.Core
{
    public abstract class SpyderModuleBase : ModuleBase<SocketCommandContext>
    {
        protected readonly IEmojiService _emojiService;

        public SpyderModuleBase(IEmojiService emojiService)
        {
            _emojiService = emojiService;
        }

        public Emoji TagNotFound => _emojiService.GetEmojiFromText("mag_right");

        public Emoji Pass => _emojiService.GetEmojiFromText("ok_hand");

        public Emoji Fail => _emojiService.GetEmojiFromText("octagonal_sign");

        public Emoji Removed => _emojiService.GetEmojiFromText("put_litter_in_its_place");

        public Task ReactAsync(IEmote emote) => Context.Message.AddReactionAsync(emote);
    }
}
