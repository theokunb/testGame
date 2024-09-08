#  Тестовая игра топ даун шутер
###  сделал на Unity версии 2022.3.25f1 с использованием URP

### Управление
- WASD передвижение вперед-влево-назад-вправо относительно направление игрока
- ЛКМ стельба по указателю мыши, игрок поворачивается в сторону выстрела. Угол стрельбы 40
### Правила
- Враги спавнятся каджые 2 сек. каждые 10 сек время спавна уменьшается на 0.1 сек вплоть до 0.5 сек
- Если враг достигает игрока то игра оканчиается
  
  ![сцена игры](https://github.com/theokunb/testGame/blob/main/Assets/Promo/Test%20Game%20Enemies.png)
- Случайным образом на карте генерируются опасные зоны (замедление и смертельная)
  
  ![зоны](https://github.com/theokunb/testGame/blob/main/Assets/Promo/Test%20Game%20Zones.png)
- Также в игре периодический спавняется бонусы:
1. Оружия
2. Баффы
- Оружия спавнятся каждые 10 сек (при том, что не может заспавится текущее оружие игрока) и находятся на земле 5 сек, после чего исчезают

  ![бонусы оружия](https://github.com/theokunb/testGame/blob/main/Assets/Promo/BonusWepons.png)

- Баффы спавнятся каждые 27 сек и находятся на земле 5 сек. Бафф на игроке длится 10 сек.

  ![бонусы оружия](https://github.com/theokunb/testGame/blob/main/Assets/Promo/Buffs.png)

- За убийство врагов вы получаете очки
1. зомби солдат 7 очков

   ![EnemySoldier](https://github.com/theokunb/testGame/blob/main/Assets/Promo/EnemySoldier.PNG)
2. быстрый монстр 12 очков

   ![EnemyNimble](https://github.com/theokunb/testGame/blob/main/Assets/Promo/EnemyNimble.PNG)
   
3. жирный мутант 30 очков
   
  ![EnemyProtected](https://github.com/theokunb/testGame/blob/main/Assets/Promo/EnemyProtected.PNG)






