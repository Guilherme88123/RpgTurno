using Domain.Dto.Sprite;
using Domain.Application.Texture.Sprite;

namespace Domain.Dto.Map.Building;

public record MapDecorationsRecord(List<PositionSpriteRecord> Decorations, List<AnimationClip> Sprites);
