FROM mcr.microsoft.com/playwright/dotnet:v1.49.0-noble

WORKDIR /app

RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs && \
    apt-get clean

COPY . /app

RUN dotnet restore
RUN dotnet build

RUN npx playwright install

CMD ["dotnet", "test", "--logger:console"]
