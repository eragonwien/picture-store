
# Sets environment variables
export PICTURE_STORE_HOME=/picturestore
export PICTURE_STORE_MEDIA=/media/mybook/picturestore

# Prepares directories
mkdir $PICTURE_STORE_MEDIA/uploads
mkdir $PICTURE_STORE_MEDIA/downloads

# Pulls then deletes current web api container
docker pull eragonwien/picturestore.web.api:latest
docker stop picturestore.web.api-container
docker rm picturestore.web.api-container

# Runs new web api container
docker run --name=picturestore.web.api-container --restart=always -p 5000:80 -v $PICTURE_STORE_MEDIA/uploads:/upload_files -v $PICTURE_STORE_MEDIA/downloads:/download_files -d eragonwien/picturestore.web.api
    
# Pulls then deletes current workerservices container
docker pull eragonwien/picturestore.workerservices:latest
docker stop picturestore.workerservices-container
docker rm picturestore.workerservices-container

# Runs new workerservices container
docker run --name=picturestore.workerservices-container --restart=always -v $PICTURE_STORE_MEDIA/uploads:/upload_files -v $PICTURE_STORE_MEDIA/downloads:/download_files -d eragonwien/picturestore.workerservices