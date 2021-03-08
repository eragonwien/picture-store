# Pulls then deletes current web api container
docker pull eragonwien/picturestorewebapi:latest
docker stop picturestorewebapi-container
docker rm picturestorewebapi-container

# Runs new web api container
docker run --name=picturestorewebapi-container 
	--restart=always 
	-p 5000:80 
	-v /home/pi/picture-store/upload_files:/upload_files 
	-v /home/pi/picture-store/download_files:/download_files 
	-d 
	eragonwien/picturestorewebapi
