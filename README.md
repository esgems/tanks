# tanks
Tanks

В папке "Resources/Configs" находятся конфиги для танков и их пушки.
	* Для танков можно задать конфиг башни, префаб танка, скорость движения и поворота
	* Для пушки можно настроить снаряд, скорость снаряда, скорость вылета (мощность пушки),
	гор. верт. лимиты (-1 - без ограничений), скорость поворота, скорострельность (-1 - без ограничений)

В папке ресурсов так же хранятся префабы снарядов, UI, VFX

Game:
При старте сцены отрабатывает "GameManager", где происходит инициализация UI, Спаун и конфигурация танка.
AimSystem - компонент прицела (SingletonComponent)
BulletsVFXManager - взрывание снарядов

LookTarget - вручную или через код задается наблюдатель и его наблюдаемый объект (например, камера следит за танком).

ObjectsPool - простой пул объектов (в демке используется для снарядов и vfx). Только PoolableObject может храниться в пуле.

BallisticMath - все рассчеты по баллистике

SingletonComponent - наследники на сцене будут иметь тоолько 1 экземпляр (дубликаты удалятся)
