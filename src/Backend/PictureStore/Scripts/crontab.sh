# Reboots machine at 4:05
0 4 * * * /sbin/shutdown -r +5

# Pull new docker images and start docker container after reboot
@reboot /home/pi/picture-store/setup/start.sh


