# Pulls then deletes current web api container
docker pull eragonwien/picturestore.web.api:latest
docker stop picturestore.web.api-container
docker rm picturestore.web.api-container

# Runs new web api container
docker run --name=picturestore.web.api-container 
	--restart=always 
	-p 5000:80 
	-v /home/pi/picture-store/upload_files:/upload_files 
	-v /home/pi/picture-store/download_files:/download_files 
	-d 
	eragonwien/picturestore.web.api
