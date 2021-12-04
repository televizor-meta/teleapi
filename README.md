# teleapi
Web API of TeleTwins service

# Настройка Kubernetes

### Установка kubernetes ###
```bash
sudo apt-get update && sudo apt-get install -y apt-transport-https
curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
echo "deb https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee -a /etc/apt/sources.list.d/kubernetes.list
sudo apt-get update
sudo apt-get install -y kubectl
```
![Image alt](https://github.com//televizor-meta/teleapi/1.jpg)
![Image alt](https://github.com//televizor-meta/teleapi/2.jpg)
![Image alt](https://github.com//televizor-meta/teleapi/3.jpg)
![Image alt](https://github.com//televizor-meta/teleapi/4.jpg)
------------------

### Создание директории по умолчанию и конфиг-файла kubernetes ###
```bash
mkdir .kube
cd .kube/
touch config
nano config 
chmod 777 config
cd ../
```
![Image alt](https://github.com//televizor-meta/teleapi/5.jpg)
------------------

### Проверка работы ###
```bash
kubectl cluster-info -n team4
```
![Image alt](https://github.com//televizor-meta/teleapi/6.jpg)

```bash
kubectl describe quota
```
![Image alt](https://github.com//televizor-meta/teleapi/7.jpg)
------------------

### Запуск kubernetes ПОДа ###
Чтобы работало, необходимо использовать докер образ с непрерываемым ПО, иначе контейнер уничтожится и ПОД не запустится
```bash
kubectl run hadoop --image=vitek999/oil_spill_detector -n team4 --requests="cpu=2,memory=1G"
```
![Image alt](https://github.com//televizor-meta/teleapi/8.jpg)

### Expose порта и запуск сервиса ###
```bash
expose pod hadoop --port 8080 --type=NodePort -n team4
```
![Image alt](https://github.com//televizor-meta/teleapi/9.jpg)

### Проверка работы сервиса ###
```bash
kubectl get services -n team4
```
![Image alt](https://github.com//televizor-meta/teleapi/10.jpg)


