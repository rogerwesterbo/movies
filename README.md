# Movies

Just testing .NET 6, GraphQL (Hot Chocolate) and MongoDb. Inspired from NDC Oslo 2021.

- .NET 6 (https://ndcoslo.com/agenda/whats-new-in-aspnet-core-0obb/0jizoqcqik0) 
- GraphQL (https://ndcoslo.com/agenda/building-a-graphql-api-in-c-0n5x/0q1z6xe8c3g)
- Mongodb (https://ndcoslo.com/agenda/zero-to-document-hero-intro-to-mongodb-and-net-0uh8/0fnkf84a2ur)
- Github Actions (https://ndcoslo.com/agenda/github-actions-devops-pipelines-as-code-using-c-0aua/0cd7e3bj7jx)

# Prerequisite
- .Net 6 SDK
- Docker-Desktop

# Run
- ```<repo root> docker-compose up```
- launch browser with sites (Accept self signed certificate :see_no_evil:):
    - https://localhost:44300/graphql to test graphql
    - http://localhost:8081 to use Mongo Express
    

# Contibute 
Open ```<repo root>\Movies.sln``` in Rider, Visual Studio or Visual Studio Code, and code away :smile:

## Data source for movies:
https://www.kaggle.com/shivamb/netflix-shows/version/5