namespace Core;

public record Portfolio(
    User User,
    List<Holding> Holdings
);
