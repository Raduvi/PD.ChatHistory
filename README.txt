# GENERAL APP INFO
The project is a .net API with Onion arhitecture.

# HOW TO RUN APP
Run solution from VS.
Swagger UI should appear where the two endpoints can be called, one to get all rooms, one to get details of one room.
For get by id and date, use the current date to get populated entries under the format MM-DD-YYYY and id 1

# POSSIBLE FUTURE NEEDS, LIMITATIONS

# Maybe caching the returned history data for a generic interval would be helpful
# To Add unit tests to Application Layer
# To Add integration tests for all layers
# Logger should be added
# No fluent validations added - Some annotations are added should be convert to fluent validations for application layer
# Seeder at -> PD.ChatHistory\PD.ChatHistory.Infrastructure\Seed\DbSeeder.cs after tests should be replaced with an actual persistence context
# Create date/ Update date is in UTC this should be discussed and see if in the History page it should be converted to chat user's local time to be more accurate upon viewing
# Directory.Build.props can be added to include an analyzer for all projects
# Does not support yet localisation