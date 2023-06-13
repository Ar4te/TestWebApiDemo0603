FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone

WORKDIR /app
COPY . . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "TestWebApiDemo0603.dll","-b","0.0.0.0"]