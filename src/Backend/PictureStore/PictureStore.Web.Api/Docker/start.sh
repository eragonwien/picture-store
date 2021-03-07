# Pulls then deletes current web api container
docker pull eragonwien/picturestorewebapi:latest
docker stop picturestorewebapi-container
docker rm picturestorewebapi-container

# Runs new web api container
docker run --name=picturestorewebapi-container --restart=always -p 5000:80 -d eragonwien/picturestorewebapi
