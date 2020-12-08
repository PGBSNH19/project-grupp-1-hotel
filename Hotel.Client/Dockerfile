# Here, we include the dotnet SDK as the base to build our app
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
# Setting the work directory for our app
WORKDIR . /

# We copy the .csproj of our app to root and 
# restore the dependencies of the project.
# COPY /BlazorApp.Client.csproj .
# RUN dotnet restore "BlazorApp.Client.csproj"

# We proceed by copying all the contents in
# the main project folder to root and build it
COPY . .
RUN dotnet build "Hotel.Client.csproj" -c Release -o /build

# Once we're done building, we'll publish the project
# to the publish folder 
FROM build-env AS publish
RUN dotnet publish "Hotel.Client.csproj" -c Release -o /publish

# We then get the base image for Nginx and set the 
# work directory 
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# We'll copy all the contents from wwwroot in the publish
# folder into nginx/html for nginx to serve. The destination
# should be the same as what you set in the nginx.conf.
COPY --from=publish /publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf


