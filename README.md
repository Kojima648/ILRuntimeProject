# ILRuntimeProject

#### 介绍
基于ILRuntime的热更新框架（定制）：

1. Unity完整的资源热更流程（热更下载中断处理，资源校验，热更回退等）
2. 基于ILRuntime的代码热更
3. 自动生成热更包及热更配置表
4. ab包加密及资源解压
5. Protobuf序列化

#### 软件架构
![http://www.sikiedu.com/files/course/2019/04-29/170724c2559d712571.png](E:\Files\XMind\170724c2559d712571.png)


#### 安装教程

1.  使用本地服务器，如：http://127.0.0.1/hotfix/

![image-20200521163436553](E:\Files\XMind\image-20200521163436553.png)

​	2. 配置需要打包的目录

![image-20200521163709531](E:\Files\XMind\image-20200521163709531.png)

		4. 使用Tools 里面的打包

![image-20200521163802183](E:\Files\XMind\image-20200521163802183.png)

5. 生成的文件夹，存在与 Assets 同级下

![image-20200521165030861](E:\Files\XMind\image-20200521165030861.png)



MD5文件如下：



![image-20200521165010718](E:\Files\XMind\image-20200521165010718.png)



6. 如下演示热更项目中修改代码，并生成DLL（需要用工具改后缀为byte）![image-20200521164216696](E:\Files\XMind\image-20200521164216696.png)

![image-20200521165254637](E:\Files\XMind\image-20200521165254637.png)

![image-20200521165437278](E:\Files\XMind\image-20200521165437278.png)

![image-20200521165452669](E:\Files\XMind\image-20200521165452669.png)

![image-20200521165520094](E:\Files\XMind\image-20200521165520094.png)

* 现在只是改了代码，作为第一次更新。
* 接下来使用，Tools -> 打包热更包

![image-20200521165714367](E:\Files\XMind\image-20200521165714367.png)

* 这里使用MD5，来做校验，排除没有修改过的资源
* 打包的热更包在这里：

![image-20200521165830332](E:\Files\XMind\image-20200521165830332.png)

![image-20200521165959253](E:\Files\XMind\image-20200521165959253.png)

 把XML 里面的，<Patches> 复制到 ServerInfo.xml里

结构如下：

![image-20200521170133025](E:\Files\XMind\image-20200521170133025.png)

把StandaloneWindows64 和 hotfix 复制到：

![image-20200521170411692](E:\Files\XMind\image-20200521170411692.png)

7. 打包之前：把AB包都复制在Assets\StreamingAssets下面

   ![image-20200521170539108](E:\Files\XMind\image-20200521170539108.png)

8. 现在打包后运行如下 

![image-20200521170713652](E:\Files\XMind\image-20200521170713652.png)

	9. 现在已经是更新后的代码了

![image-20200521171311163](E:\Files\XMind\image-20200521171311163.png)

10. 如何回退热更新，很简单，注释掉ServerInfo.xml 里面的，patches标签

![image-20200521170855204](E:\Files\XMind\image-20200521170855204.png)

![image-20200521171410078](E:\Files\XMind\image-20200521171410078.png)



11. 请注意，标点符号的改变
12. 如果还有一次修改，把0.1版本下创建文件夹2，在serverinfo里新增<Patches>标签，Version 改为2就可以。
13. 回退注解注释掉<Patches>标签级可
14. 视频演示和解说：敬请期待









#### 使用说明

1.  xxxx
2.  xxxx
3.  xxxx

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 码云特技

