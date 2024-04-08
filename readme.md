# 火柴人vsCAD

This game is a Project for 2024 THU software engineering contest.💻

Uses Unity to develop a 2D game of Stick Figures and CAD.💾

Credits: TUOMF, Xyberoid, KnightNemo🧑‍💻

## Overview

* 场景中包含了**玩家**，**鼠标光标**和很多**物体**和**敌对单位**🤖

* 玩家有四个技能可以使用:
    *  左键发射**删除**(攻击)🖊️
    * 按X准备**传送**(再按一次取消准备),再按空格移动到鼠标的位置;🎃
    * 按E准备**分解**(再按一次取消准备)，再按空格会激活`Tag=="Array"`的物体掉落;👾
    * 按F投掷**圆角化工具**，可以把尖刺(`spike`)变钝使之无害👻

* 技能的释放部分都在`playercontroller`脚本里，都有对应的函数(`Teleport Erase Breakdown`)，删除和圆角有各自的计时器⏱️

* 玩家是单例，`playercontroller.instance.`可以在任何位置拿到他`public`的东西🏃

* 技能希望加上一个获取后才能使用的设置，技能图标，解锁情况和使用次数在UI显示🔐

* 其他的组件暂时不用做什么调整🤗

## 0219更新:
* 增加了有HP和技能的UI/复活🗿

* **HP机制**:显示数字和相对应的六边形数(用的不是预制件)❤️

* **技能机制**:
    * 未解锁前显示为叉和"Locked"，解锁后叉消失，显示"x/n"，n为可使用总次数，在`playercontroller`的`chances[]`数组里,`x==0`时图标蒙灰，且无法再使用。
    * 目前n暂时默认为5，在`eraseChance`等变量中修改。
    * 使用技能事会出现提示框(预制件)，3个以上或4s以上自动消失(在`Console0Script`和`TipsScript`)里
* 死后点击右上角按钮本关重新开始✝️

## 0220更新:
* `DontDestroyOnLoad`仅包括`LogicManagers`和`Canvas0`，其他需要共用的每个场景单独放(包括`Camera`和`player`相关的)，`player`的数据可以放到`LogicManagers`里的`Logic`的脚本`LogicScript`中。🤯
已实例化可以LogicScript.instance引用

## 0221更新:
* 增加了**记录点**(文件保存的图标)，玩家碰到后消失并将这个位置作为重生点。在实际关卡设置中应避免玩家回到上一个重生点。
* 改进了**分解**技能，如果绿色的探测区域没有接触到阵列物体(蓝色区域不会产生)，则不会消耗使用次数。
## 0302更新:
* 完成小boss: `Terminal`的制作,`Terminal`在玩家进入特定范围后进入`ActiveState`,能发射6种不同的炮弹:
    * `rm`:发射正常的炮弹，击中玩家后玩家`HP`-1🚀
    * `:wq`:发射的炮弹会短时间内禁用玩家的所有技能🈲
    * `rm-f`:发射致死的炮弹，提前会有瞄准射线提示🔔
    * `tac`:击中玩家后让玩家倒立，此时可以使用技能、左右移动,但不能跳跃
    * `--help`召唤小兵,仍在开发中🔧
    * `move`:将玩家随机传送到与当前位置$\Delta y\in (0,10)$,$\Delta x\in (-5,5)$的位置
## 0303更新:
* 初步加入了护盾系统：
    * 玩家按右键可以打开护盾（再按一次收起），护盾会阻挡与之接触的射弹，使之消失。
    * 每拦截一个射弹，玩家的护盾数量-1，会有护盾变红和重新展开的动画。护盾数量为0时不能打开护盾。
    * 场景中有shieldstore物体，可以补充1个护盾。
    * paperchar射弹不会消耗护盾数目，C撞击产生的爆炸会消耗护盾，但不能被护盾阻挡。
    * 鸭子可以被护盾击退并阻挡，不消耗护盾。
* 继续完善`Terminal`的制作：
    * rm-f重制，改为发射一道很粗的激光，接触秒杀。
    * help初步完成，向玩家投掷，接触到任何物体或存在超过2秒就会召唤鸭子，C#，C++或C，后两个几率较小。
    * 大大加快了发射射弹的速度，wq和tac的作用时间延长。
* 场景切换器加入贴图，作为彩蛋存在
## 0304更新:
* 重制了火柴人的骨架及部分动画
* 增加了`getbreak`等4个获取技能的预制件及换关的`next`
* 将场景中的对象进行整理，关卡本身放入空父对象`BackGround`中，每关需要加载的放入空父对象`ForEach`中
* 新增了一些`Scene`，分别是只有新角色、两个角色都有、只有老角色
* 待完善方向：
    * 火柴人的骨架动画不完整
    * 火柴人的脚本执行还有些问题
    * 获取技能、过场的动画
## 0307更新：
* 新的火柴人跑动正常了，跳跃动画等还待完善，目前仍然以之前的人物进行测试
* 加入了另一个彩蛋Empress of Light，目前在`Scene02`中进行测试：
    * 尽量还原了`Terraria`中的招式和行为，以大量的弹幕为主要攻击手段
    * 目前以玩家作为目标，在BOSS完成后可将`Target`换成boss，最终会帮助玩家打BOSS
    * BOSS关里作为彩蛋出现，召唤部分尚未完成
    * 所有射弹都有很明显的发光
    * 由于还在测试阶段，按K可以将其秒杀以检查死亡特效
* 加入了发光材质
    * 需要场景内放置全局的volume设置炫光效果，配置文件已有
    * 需要main Camera打开`Post processing`
    * 材质`Light texture`发黄光，可以快速应用于物体上
    * 需要变色等复杂操作的物体，采用普通材质，通过脚本将material.color的r，g，b通道设为大于1的数就会有相应颜色的发光，非常明显
    * 已经为存档点，过关点，光标和Terminal的激光加了发光
* 注意
    * Terminal现在不知为何靠近时不会启动，注意维修
    * 需要制作一个启动界面，此时UI尚未入场，不能直接用logic
## 0310更新
* 新增标题界面
* 标题界面点进Start后火柴人跑到屏幕外即可切换至下一界面
* 更新完成`player`的动画，作为`NewPlayer`放入ForEach中（暂时没做成预制件）
* 未完待续：
    * 用传统方法点击按钮后火柴人火柴人不会移动，原因不明，暂时用动画替代
## 0310更新2
* 正式加入了第一关
* 加入了静态的三角形和圆形，可以和原先的bar共同搭建基本的场景
* 加入了提示，存放在tips文件夹中，可以贴在场景里
* 人物模型换新，开始采用新的火柴人，骨架动画基本正常
* 跳跃检测更新，持续踩在地面上时也会设置`Canjump`为true，减少了由于地面不平整等原因导致的人物在地上却不能跳跃的bug
* 现在玩家即使不主动跳离地面也能在空中跳一次
* 注意：目前发现人物在有复活时，进入下一关时会出现在上一个存档点而不是预先摆好的位置，目前通过直接移动玩家的方式予以修正，但真正的原因还没有找到🔔

## 游戏在0310第一次送审，希望一切顺利❤️

## 0313更新
* 进行了很多修补工作：
    * `fallingblock`现在不用手动指定玩家，且可以设置触发距离，以及玩家是在上方触发还是在下方触发
    * `fallingspike`进行了同样处理，不过没有触发方向的选项
    * `Duck`现在可以设置攻击值，在一些情况下可以秒杀玩家
    * 玩家现在除了`fillet`之外的技能数目都会在进入关卡或重生时被重置为0，避免玩家利用死亡直接加满技能数目，记得在保存点附近放上对应的storage
    * 加入了`breakstorage`和`teleportstorage`用于补充玩家相应的技能数目
    * 背景图片的大小限制改为4096，避免其被压缩，清晰度得到提升
    * 玩家现在可以正常的在`fallingblock`上跳跃
* 加入了传送抑制器，可以调节作用范围，玩家被抑制时会发紫光
* 加入了另一类尖刺`movespike`，整个都是红色，触碰任何位置都会被秒杀，且不可圆角化。它们中的一些具有特殊的脚本，会在玩家试图跳过它们时进行移动来坑玩家一把。
* 加入了会旋转的四角尖刺，之后还会加入更多障碍物类型
* 完成了Scene04和05
* 加入了特大地图背景，未完成的Scene03已经在使用。可以使用`background`中的图片在图片编辑软件中拼出其他大小的地图。
## 0316更新
* 制作了Boss的动画：
    * Boss名为`Namukp`，即`pkuman`倒过来，放入场景`Null`中
    * 制作了各个动画，其中`Shoot`和`Blow`区分左右，`Jump`和`Fall`构成完整过程
    * 创建代码Bosscontroller
    * Boss有关内容均放在`Assests/Boss`中
* 这次更新有点水，真的很对不起😰
## 0319更新
* 进一步完善了Boss的行为：
    * 已制作函数包括`MultiShoot()`（这个应该基本没问题了） `Move()` `Jump()` `Blow()` 未完成`Laser` `Shoot()` `Eliminate`
    * 动画播放有点问题，可以的话拜托修一下了🙏
    * 目前行为逻辑是横向超过一定距离启动`Jump()`从player上方落下，小于一定距离启动`Multishoot()`
    * 在`OnCollisionEnter2D`里面有击飞玩家，但没有效果
    * 因为碰撞体的形状不能变化，所以暂时用外围的环来掩盖这个问题
    * 在Edit-Project Settings-Physics里可以设置不同Layer是否碰撞，目前取消了Boss和bullet图层间的碰撞
* 以上左右更新均在Assests/Boss中

## 0325更新
* BOSS完成了大部分制作：
    * 移动：向玩家走，距离较大时跳向玩家，落地时砸出红色的冲击波造成伤害
    * 攻击方式： * 如果离玩家较近时，会击飞玩家（Blow）
                * 正常情况下，每8秒在下列攻击中选择一个执行：
                    1. 发射六组扇形弹幕(Directshot)
                    2. 发射大约100个小圆射弹，画出以一个参数方程确定的曲线(Multishot)
                    3. 发射一束激光，激光会扫过一定角度(Laser)
                    4. 生成四个四角尖刺，它们的轨迹的x,y均为一个只有五项的傅里叶级数，每一个an,bn为随机生成的整数(RandomF)
                    5. 瞄准玩家发射两束蓝色激光，玩家被击中后会处于被选中的状态，受到的伤害*3；`Final barrier`被击中会处于被选中的状态(Select)
                * 每次受到玩家的攻击，`energy`+1，达到5时释放大范围伤害技能(Eliminate)，玩家必须找到绿色区域并待在里面才能不受伤害。被选中的`Final barrier`会在过程中消失
    * 血量：1000 死亡后会等待几秒后消失

* 最终关卡流程完成大部分制作：
    * 玩家需要诱使Boss选中四个`Final barrier`，并利用`Eliminate`将其消去。这个过程必须按照次序。
    * 四个`Final barrier`都消失后，关键部分的红色屏障（秒杀区域）会消失
    * 玩家需要先捡一把钥匙，解锁获得全选删除的位置（在上方工具栏处）
    * 玩家找到全选删除后，左键发射一枚橡皮擦，此时它会向上飞行并发红光，之后会有很炫的特效出现，Boss在过程中会被强制秒杀
    * 此时维度控制框可以进入（会显示一个闪烁的绿色的火柴人标志），玩家进入后脚本的操作就会停止
    * 控制框和玩家一同上升，之后偏转，发出明亮的闪光，留下一个时空的破洞，把玩家送回去。由此跳转至结算页面。

* 彩蛋Empress of Light和Boss联动：
    * 考虑到Boss移动较慢，现在Empress of Light会交替选择三个Boss上方的位置向其移动
    * 玩家可以捡起一个罐子（七彩草蛉），按Q丢出去后召唤Empress of Light
    * Boss与Empress of Light会自动切换目标。后者如果先死了，前者会将目标重新设为玩家；前者如果死了，后者会自动停止行动。
    * ！！！！目前仍然有一些需要对方数据的射弹会因为一边死了而报错，这个必须尽快修复！
    * 这一彩蛋还未完成。特别是Empress of Light被打死后还有东西 :)

* 仍需要加入的内容：
    * 摄像机自动变焦
    * Boss被卡住时自动脱困
    * Blow的判定（现在还不太行）
## 0328更新
* 音效制作（都在Scene10里有）
    * 玩家音效播放正常，boss有严重延迟（暂时没有适配，只是确保每个动作有不同的声音）
    * 由于`NewPlayer`不是预制件，每个场景脚本中`sounds[]`的修改是不同步的，目前是在Scene10里修改的（可以考虑把NewPlayer做成预制件，根据`sceneNum`给`chances[]`等赋值）
    * C++播放正常，C不知道为什么没有声音，C#声音可播放，声音多了会显得有点吵（目前Volume为0.3）
    * Scene10中有一个圆形对象，按QRS三个键会发声，测试用
    * 从Opening开始自动循环播放BGM，只需将音乐文件拖入场景创建对面并作为LogicManager的子对象即可，目前用初审视频的bgm代替
    * 在Sounds文件夹里有很多音效，可以听听看
    * 在Prefabs/Sounds里有音效对象的预制件，可以直接拖入`sounds[]`中
    * 现在使用了另一种方式，即建立`AudioClip`的数组`clips[]`，但boss延迟依旧
* UI完善
    * 新增盾的图标（我搜shield第一个出来就是这个，就借机整活了...有更好的可以改）

## 0401更新
* 音效继续完善，玩家现在有了死亡音效和捡起技能，补充技能的音效等。
* BOSS的音效更换，目前延迟已经解决。
* 加入了游戏开发者之一T.M.F.。在BOSS关下方可以找到一个门，会将玩家送到他的办公室。此时BOSS会暂时待机不动。
    * 点击开发者进行对话，他会给你Empress of Light的召唤物并给你开第二个门，可以由此回去。
    * 如果Empress of Light不幸被打死了，他会用脚本把BOSS正义爆杀 :)
* BOSS跳跃改进，可以穿墙以落到玩家附近的高度，但在一些复杂的地形仍然会被卡住......
* BOSS关在搭建中。目前BOSS过于逆天，可能需要加入定时刷新技能补充物品的点位......

## 0404更新 游戏结构完成闭环
* BOSS关大致完成搭建：
    * Boss 跳跃机制修改，仅在最顶层且玩家也在这里时才会跳跃，否则直接传送到玩家身边，避免被卡住
    * 加入储存罐，一种可以无限刷新的技能补充，一共四种，分别补充血量，橡皮，传送和护盾
    * 目前最终关卡已经通过了试玩，将BOSS卡在钥匙下方的凹槽处可以等待其发射选择光束。
    * 将Boss击飞玩家的力度调小
* 结算页面加入：
    * 玩家进传送门或启动原神后触发结算程序
    * Scenemanager对象负责将结局类型等信息传给结算页面内的对象，因此会在UI等基础框架销毁前一秒切换场景
    * 三种结局都对应一个插画（我的美术水平.....）
    * 按`Back`回到opening
    * 考虑之后加入选择关卡的页面
* 目前游戏已经可以不经退出循环游玩 :)

## 0405更新 
* 新增关卡Scene06
* 增加选关场景`Select`，相关素材在Assets/Art/Sprites/Select中，我下次做
* 新增`death`与`score`的计数，在UI和结算页面显示
    * 其中death试图用计时器实现数组从0到结果的自然滚动效果，但结果是不显示
    * score的算法目前比较简单，是用erase击中duck/c/c#的次数和，后续可考虑结算时加入剩余未用技能数等

## 0409更新
* 完成了Scene03 Scene07
* 继续加入音效
* 小BOSS `Terminal`完成制作，加入光效，音效，以及受伤和死亡特效。只有用分解技能打碎的array接触它才能使其受伤。
