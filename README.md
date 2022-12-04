# Flare.Battleship

## Program Design

1. basically follows the CQRS design pattern that models the system as when taking `Command` mutate ssystem state, while taking `Query` serving the system state information
2. seperate project into `Domain` and `Application`, try to follow the DDD guidelines
3. Unit test has 100% code coverage, and moderate to hight test coverage
4. the Console program is just demo of the usages of the library APIs. it would be enhanced if using a DI container, mapping library etc.
