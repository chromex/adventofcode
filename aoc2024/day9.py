import aoc

input = """6453654696157350383447494358714973307737146214411780208117985376687839441238957892445322706764254967526860666850486058293261435678119395528290882260118480719212282135614795209581402116449048996963765124755545119490874694309592464838144172457683298775923723469961698243874554434415583132321718177017572436369146324426826592764823738799882013292177148231817831719812131649681575573020372155753210867246551550922741203352777392613454428365246639209824538037688816835060144865654988309348392970481284641545181368978324419751252888338590536075278495313890132697788116126488777977704595461169562751572791148465625922317793721451444220296633804933457486142377987487909565344929263028918765566466992789781622642286824114238136798243499169997016891488653611637958776126272798564426311773357840573728131168846258536354272367207235243546618953376093249739926245848275788361281698605595285124671730282598857848238325341586456212423932601268945572137513472270846275529388692061444662589028499926734762511835351218974274119429838370101652893749248622295477997718715654576995132230452780316867839846609787164782101255428251304023351316683254655645359919194283478194811177133822553383431086563078304440112945347882821480871747961722451750507664732287634549639794151248338257517082111450926457441311551322248537942781344935159571872266592457147317793340382268808746188832637563227681116364852138458414281184514448393777367287606219633269369844958835723089402613534763989287527897179416597365385870271228468348442823994353788042526232603560833297733140419074487419947522836294641981937342297933248490919913842269481137764979215336224776839637897217828456179329942857628284456192988192255694251577564668331621417556596739103198936895361933786239568137455274268346931331894856899147486642647450612559951538322881844544316548522394676034286113605659273215539623911259146218346298545143253621758156345026517669603764348220993823643467864396702963236519667591397042734454377227922743434112226373781479535054986262884537579828396781358284375219808999491937954174123452653061242019268623631587358243738388884076799545682193851330166842583467678333738058829870283382125990168798154988452583882910527841251353673691847573568430585634976549107562977857264138968884334959606592173625453892742322803168121726993997316888649017843349867542745656596111221268432168347899476260856142497849758339975320804482839730926958684433527329577023997050544631516030244123206719236227678636579680615774326144801188524051354794941747996947526185693833303543155265739494984512873399601837393777453034374939803743818381535829741425855858825953996317751145275831169544517689163157316393159557177432443738487639528693831775443422478855467123398858116722354821334153498956238367129957673060509152806182598282872046283643606562682466474613181724217110579375329726615056814285731263178864382037252857453641324883242273859976788551807664297871911361189331431322695755318481234040317737489858727916164047815954939467574113248746828190566526798179547784113645702961699081574969994684129828332483659034325466745846124885418347325532347216975245619952409553914146833560238370141857463628569110483733216847453324842959932453231346946059321925747763644288674921669274799770248695394096473921655057842281291477866630353247617388747167125954915264417060726435577173566149726326649310511530142421465070967981711262424113987287836854455282769393726417133751434265659847852154474459952089372744468792716891261448718157949990349351291860467350567475293557146655711033829113131075352547859362497057106626908910857396918130894110455665423619915253271119578235472759497459955459136491482679465253845727254633139091277354153450227914706941677810971623784995635716959222259544155497321325397725471680808199866856494673751540672740576363115826541125611594524468693965711568311623739641366216914759747846626363217676666839448919928827771521668883585860322046457785169370895440122560472564279179311261151236785075765629386638382648151356198020771341618467552158846573388574299020385473904697167371548663211565131722228426427891757930861481598029724232117249713691531682184131855847534269397465446462494394319931638556767359293874937068171918855999157423262448546125996144856222823276881286882667935234289587242147685194881516397326614319941371936242727930121642438772459497433966337430102554643926779446592621229550594228687415875220185682668510386496743659539792125830378340243991583391179028831356773836973489211846329331602623924915113497165252543932135692934226223882234220769099732113823929817366967628623450807793436238521473824560276010422526114936426315512192308157981252707828934935587391321354133126678481512436456434829062937578704761503526256331795194941069115293746363752777564564249495925872531991366435717876935168598971114222844546687727318790743094899326498545599389966477136088258055953365932347898793486937629175591145356957979583321787798293227483221131196815562644328982594652707958642091604814311231286240879385505499904741644669973183777983348619838865938934125438225799496338889325516092361624584118939642669477593657947798498485582687878860494584391168428593746424715641141363164239596254863971948447284669798651718367928667666695847748116136774443194055401533945691623590287063494766465774381582736813863093877851587612524023493254629792552892182830468827285696876581138620834678186923164512247341395099905042984892131145493140629055769043291954272545145217255534959865547149283451652768117723402432796733187366103654651819542212122448959199136277855648181159397241631559527356781131306699579983711437392780123460135323175879216778266992781098534174702666113231113592631815368176783321187378898729343737912696577081999095757940579376806243189263424339419967596868475833733426739318368381766827818567161161357384388572529972476775824240667637434438171425574722493521861692708393239949903450796022889963808347167217626712365268822814945693308744599035628321924079388831538512189723838481636541985689686879992317487789546333915638903670853496836552983645516030392064621379379240221430245047332390618781613321646927619756307337302266439123304711322879765171701612694991452194397356265511859090337687835427576392571369904885581057344561698737454470845374638714125428583613843287143984662923794833895547388392416317702095324771719139725034699742757255593139352061398236728641949865162564821511738396382443754370275577258382281892646261386796889647902811133424871973361082359155825995794653683788427412741867295739123230601190542033483051639243957734744390907658786280747312201925821275251837299891782443796385248343616675967177857397289634997618884947878063475885396818338820584112117257416478934047166771363782884193689541967351664564865258518361676546883243418514182775278089467622882972938227635422413550249243539621754797995549767955488295492365597353161859491831837739767749167177189057193293155891192837332361384489836760587123917922256353437864611016276963582661738590794328764048809254646954624564929959621930731788667318604186989683229449545382866810125921166240817165665149973275748947517085479634643143646781589035363865188436897821335541787277778671176955184334206884135612181496248059614257723837111981192824684879989232531034174297816135293785292454136424941231934646939069431352726996779325851884984041379050636193213157753782163322138960301636617891601333991373478685858940227463883546669453549560913479641454825692953638669498878658311873118137396118103179907649237638701396742464108282366475791321786091283465836677587128824520193143597950222797963327444119663124553880172818555969503341771892776191339986737597586934796419538430784520255131328360129010827634692720446024956178656221872287104011751082406886733792372755406312518719843874364652203861454269199135812666891140262384102739881484507860154915692790495623713188291126158882832042429758568272602496203565823273466056561054591213614195641436135598914230589214919679266975355499853114855950228674895797732510986431119643256047456269743421572155333437496493612596852116108311463473854755933288288021962145121717592451364960154441668468874998702484539286763151817712105481736629923491303417332043386745235195921048471518589963334690533516474758646010957873933979853091485476984986227096653661721197997236757932521771259684162033588868823894307654734815507572231391614030139646518162168718866188608975471280532116498958732487816994756797205515414647737855146116793461433671138933949657581735842066852897433561936215892358848377443583289030533012178586763520175855212846881471811376681184701393274144402250152652967536269882516611648445478520427562886434559585127139185173588814464695404529871383252331581399622045692444878694181392139794423091686533535692448084371527777964644573236460227458442919877737505554664236619179545215641583653522913625397653843741778293652824125024606518341053572358232293242161651835983767495212722098227613974951919290663181684930508480865717831224602447434077231816534824747211884073839876564621433747694999833467754985312252117573156994651820629579491040383948813014712246935066175592885528842335492036288482714670565739436344926620261161582850496996359780553148669517531467437542767094539988262799415640838150671747342930341544154870502321617195804730845716334868182879754571253341362025578798765524698960769868392724853359256893746985478421121995317345724363979343162486336442849322681284415593596126959987509165366454234363881756236422172270109017558582786277627261885860913136447579585276873769643235881418633119701666945361758614306236322725602451585646612141622996809617661238939570322121446964747920909399681981793618236120298016819388929551296453662530184360244761292980961659499111191245285165482299408239859382989661302673571943398984141324438982557911223556721954717548792010533544384615386436164871433933101284669892529895487060767596173115332065931481922966772468391129868435499614994161972919928691957010549624575873737192798848517493591853425887456974857514425683896948808864755232887828517476194734586293392236177920826686543650858696524351776519349392782951127937352230894583522265653475611646871070432349612283935519645849307393674297318223117776418492284681955857206485439133274198553514656048587247117196615268459511398489822122642519642326464289272742572056973495252056294882818713207692459941937047843860961742859141141873757286868711727912179522501839911340178855609421648212455484139718686049426424898852739753314868391954728987472654242357188613793721363672665998811630838110938599132373626358529661925646477741985120681817536275141760499068522251174827766710484041937419918120167011186933823984529110243027315357965846203656639582901614222682949029218122135973269976239290308659253256541076923514518614752951952128947746235883131945668236718446452717925988204764703719662654817058115174469369614274642038489842607645337957565261275527226991316631701569863318943043253147627653267676435574505357898962344212587355982732344625707065175558999035801411756779825578581410128827831412263523599941928753296838313247504139619836614238721629332274325722137298675228563513961026891442942210814723225315874216806289715520342996658530718432167939963897867682803214627862179833731726675343218981653163685171642969486075801727583225122976902432313989464889284773233757903954627392798310853272427289849823621585601562883437273171703712122984638981262633666375196253326622526520193544538263179293477193127770818496728621953656227612419211498796668044817263887575671683893275832638626260242891446585541411733684249231631697912875812548514427483659703136954728663259665756986766583125138537504234192472209755949853105390427815348629295037729889859396133086865775948193537784518464367749164798654671108969854188904899739775266432303225908399874946298148591959667636684498505969846551984792541815357383629945129242879881335027181174817063168563643210835782658852144281919949598964108031141229146538261567493356354988905284448680391877381211918896924870628541292456232070707632153376197383411532969737428394355217747654712312331627286628872438195929678882322343556122383361177515183891635792458416229732118055641590173534967444649297568134493268924778549224961992141238877626257633471323791899926654442833161745947184515966655690965164172927476830314216123818667019645968822518663540758036481388932256461158153580195472328166226936909862853991137958839942901873935267574221727197671115776048281439119689289319103072814040721266532116406657161387804644901288273076749637275823463594138233811870491217241955593191611666197990813579559825316150562649193732994324346191122145633581714842552950974574671365818163502953792874908886957446515936156526199224836384862672622618602991286724223587432618887579382884267530335525447852191963212437775072521984573517983093101125779267947283188752342165931013444794659750473483335128348099812346142478367778897157567025698298944381571697378128479642136644455911571694927018247621763583586843715746171941692381555438308420936796621894769018862310721989193824269531228278304783276056767750725921131169769640671393482271923463501865601134715739713564269211917157804289406292866142317492552017337463332876301580897926698069217580391242659441767757646328425347246227906953652140108090484487348416958755187156913395856065842944127492287565258615932238245776519969897477613077488829572365471361966096887980188335314929414438308596546132377550706214892252156021574475269849317014107719381798174118387888585248689493122718858292307920697689208331276373123839172588669821612958173477422147888893751467383352661061325777656426642459223160156366122668263714185555422525136563755920372322811121319818209874831149523374753830917758539914929332465413609741192440221074541053136184354513224089201990149812626733179736778684586772609677714714394455668046395213102433381823252245604521707042351289833315209683953596954624438344744356972130378723341365671342235813661886197537569194291916464388741585691226359266279487317979941747582624706772835117694675762576481684975918439636165191787134127921582128837048954317876534622567774080591811439192832561202027826444535896118564886725441957903626261524611365265237856322656194527567716558833621198812212555542959831796426813708291688878935930892278235577423631819196439687851998813916127195158314665749633914575071273557904295576044313546478570209023439399796570934321652974582599721049429913554413646133865694383394799713353219996442468565762424723294966063406281649593354633693374937371173459505352895574966010775195233759661579966427147815545463867936125250408630368492486546389884285318232628489324315115564743514685204271908644295381691358938922257863971680343160641085956957849116494046296054453374739667624145895113207243123288166665998045677036532642585743977347624457396242587029896552249674909646398292245756614446481180764693226822991988299827896022322330976470659317931159127217151894901291278373639416222782617469584819654431259223751629296793691596861898552140192112403213157436391652849457818573995665753620241510628733578396297893306279563246129359499475868446374425271462924789991230554882312241307390199250327374953043674191816179545191459464808923605346513315958133475110133396475011487817195385895629479225972562185198318446329929902650443192979583621098445181747910798417354489404440805317778978999525247867204371241129675343217380716413536398128577229625712325458265558025599725219814304495309312169629876737311550637962933132304212456919343925537043786430789230548271147565381077839592531368476597937648677459324353979755282759514965781271513342455489133274359391217080674546852525154678151964714055504246786919957625779025581597541183978914309095414638575413844378291652402328967997735144968327636743546662722356238440194666643819676098291469887999693315794638744793764515263864515921531368714425754541191480357272257757557854794150212489743616278811949257197036674831243430554380507729682178428388528929758914858326615846731891528259111238468537724465596629651637624070462857373242419022811086214995422753765169597580325479368510894278748471572576612638315398949533468359784618369426927760227471722085182699317840395143717844471013716838226711626681673430848219548180619453917853995819826269476393161077125630275693729651755023756483642342682068146591799442622342184731871731332367499871656051213517493238577567136496849717163785807813463543816981855830456246875053899935202025102610328967131895575995948954968445318679557067419764709698253838768276916854515264951433823095896641834051974444669695754851473988438511855745997633597749363617174459691733365164276140106935555987642778781258449789145883214218187649231993403659397212178723772447151142174760928047217191771658129092734799821710668539119941812911578183638976668643921058108387514037741244265842335257221318381850747427647046168929505457881069299076103666923429167475944412211835121413216069138316627753737998736754837825904711739968522066792343315747483730768589368228642619622368246979715443991585664169165419768819781824949877153035145627143734412855327211326240786528229689631214819766611371396254905771265613713751956778447455452891248256467226457378809057587971845341183029503745703822536277518799118142904962844324971490747445763055504948773680327715931320698170599676361147191138704542709330672611794580352395374931786630264668114639278264678417654866302194337114404682291927658852923628777524462750546227262059121274851226105750789489701316562840968236973880583642998369864621783836761739152995966958286110704749615010995750531092345923269214403326685319106467377284767330808162319893862993521378523851289256687663343813662566593424995999343442134837582641679540174461526790179839223654718714873399665179421753213071918322368838215287278044585565964498622512719620599155999179525239276198217866769895591614349056598817656911842836741661246362551182851464891779918770675496162510477933775653227735263976929292274182157370409825951416867361714552427549539132443120314779552568924431927255927020354894211923507968838046303799598142175415991011144626715739833918979250476761375245994618203029416657847810122693759656497038258890607819611450586198454142674878722019743334905493982445111325235052878743457365247072138620384718134380626081795359555996318460612460592210168892181039534412394128657257304040598059347643854485416097949887558628341361865437252664517952199325349285748391214598314261802877146091525830168071833335688588798720281348929422806047376149683348111484161491633720926938364220553393343899192753431278401395847997118635599741894319419622148218281785765169121836689496517671209949924677466872302054412793484873794399128823693422379234831888861772431132983685537356321520345518708199731117701921773779844946954390726519393721778437884278444993131810252252355939453489906498421790955284942444348352668442134231173845726925911335432142906673265379145825732870122248424661515973651548734752121643391029214317795957278510243767767872819466129556326239608296802665573310294318894636701189547039894614814946345897631434784764532978342973974546578041555496481792966871202428946588713621393721716791405724754839905572335413361496299719211126738542997381545075345411463883643960367462722643321439693289701756581257519885171044249889791546533497618966738697758067749063289620893491461981827673486622631537724129782448741153131655413859714028523677153698207838922948861021667261453080369944444850647160214770112050721873153216979392223449742975421589888173351841569915326856739559167273563123215010783036714563697056546283603951173241747827778715158530198341113359756765957359477667953089602173391599531093772849653748104165229220463310895169775235268247736493821984979984565639107733704243928046346235782348592972527073899657682685153656655685925428121694941683385518258520975251571175709136965256336952316784786133776741469785838038136861386989353651452955174676164165765063554613592851399887825382579469309633596914318493266454964085602086335561235813693185694029254711499130324618986273504346748348841196223120923725933098381059669421798697121568315719669655112271747547716415362593272"""

fs = []

# fill
for idx,c in enumerate(input):
    num = int(c)
    if idx % 2 == 0:
        fs.append([num, int(idx / 2)])
    else:
        fs.append([num, None])

print(fs)

# compress
fileId = fs[-1][1]

while fileId >= 0:
    idx = None
    for i in range(len(fs)):
        if fs[i][1] == fileId:
            idx = i
            break

    assert(idx != None)

    cur = fs[idx]

    curLen = cur[0]
    for i in range(idx):
        candidate = fs[i]
        if candidate[1] == None:
            if candidate[0] > cur[0]:
                candidate[0] -= cur[0]
                fs.insert(i, cur[:])
                cur[1] = None
                break
            elif candidate[0] == cur[0]:
                candidate[1] = cur[1]
                cur[1] = None
                break
    fileId -= 1

print(fs)

#checksum
sum = 0
realIdx = 0
for i in range(len(fs)):
    cur = fs[i]
    if cur[0] == 0:
        continue

    if cur[1] != None:
        for x in range(cur[0]):
            sum += realIdx * cur[1]
            realIdx += 1
    else:
        realIdx += cur[0]

print(sum)