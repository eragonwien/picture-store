# Reboots machine at 4:05
0 4 * * * /sbin/shutdown -r +5

# Pull new docker images and start docker container after reboot
0 4 * * * /shared/picturestore/setup/pull.sh +10

# Pull new docker images and start docker container after reboot
0 4 * * * service nginx restart +15