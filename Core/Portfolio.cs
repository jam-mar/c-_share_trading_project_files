namespace Core;

public record Portfolio(
    Guid Id,
    User User,
    List<Holding> Holdings
);
