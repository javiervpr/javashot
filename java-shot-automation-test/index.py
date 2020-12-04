import time
import unittest
import uuid
from threading import Timer

from pyunitreport import HTMLTestRunner

from utilities.driver_management import get_driver, take_screenshot


class JavaShotEndToEnd(unittest.TestCase):

    def setUp(self) -> None:
        self.driver = get_driver('edge', 'http://localhost:4200/')

    def test_java_shot_end_to_end(self):
        driver = self.driver
        driver.get('http://localhost:4200/registrarse')
        self.register(driver)
        self.home(driver)
        self.jugar_partida(driver)
        self.cerrar_sesion(driver)
        self.login(driver)
        self.puntos(driver)

    def register(self, driver):
        time.sleep(2)
        email_input = driver.find_element_by_name('email')
        password = driver.find_element_by_name('password')
        nombre = driver.find_element_by_name('nombre')
        apellido = driver.find_element_by_name('apellido')
        button_register = driver.find_element_by_id('btn-login')

        identificador_unico = str(uuid.uuid4())
        email_input.send_keys('prueba_{0}@ejemplo.com'.format(identificador_unico))
        password.send_keys('123456')
        nombre.send_keys('Nombre Prueba-{0}'.format(identificador_unico))
        apellido.send_keys('Apellido')
        button_register.click()
        time.sleep(2)
        take_screenshot(self.driver, 'registro')

        tituloHome = driver.find_element_by_tag_name('h1')
        self.assertEqual('Tomate un Shot de ¡Java!', tituloHome.text)

    def home(self, driver):
        time.sleep(2)
        btn_jugar = driver.find_element_by_id('btn-jugar')
        btn_jugar.click()
        time.sleep(1)
        select_alert = driver.find_element_by_xpath('/html/body/div/div/div[2]/select')
        select_alert.click()
        opcion_5 = driver.find_element_by_xpath('/html/body/div/div/div[2]/select/option[2]')
        opcion_5.click()
        btn_ok = driver.find_element_by_xpath('/html/body/div/div/div[3]/button[1]')
        btn_ok.click()
        time.sleep(1)
        take_screenshot(self.driver, 'jugar_partida')
        li_list_elements = driver.find_elements_by_class_name('list-group-item')

        self.assertGreater(li_list_elements.__len__(), 0, 'El numero de elementos li tiene que se mayor a 0 porque'
                                                          'indica que cargaron preguntas')

    def jugar_partida(self, driver):
        time.sleep(1)
        for num in range(1, 6):
            time.sleep(1)
            li_list_elements = driver.find_elements_by_class_name('list-group-item')
            li_list_elements[1].click()
            time.sleep(1)
            btn_siguiente = driver.find_element_by_xpath('//*[@id="ulListGroup"]/li[1]/span[2]')
            time.sleep(1)
            btn_siguiente.click()
        time.sleep(1)
        titulo_resultado = driver.find_element_by_tag_name('h2')
        self.assertEqual('Resultados de la partida', titulo_resultado.text)
        driver.find_element_by_xpath('//*[@id="ulListGroup"]/li[1]/span[2]').click()
        time.sleep(1.5)
        driver.find_element_by_xpath('/html/body/div/div/div[3]/button[1]').click()
        time.sleep(1.5)
        take_screenshot(self.driver, 'partida')

    def cerrar_sesion(self, driver):
        time.sleep(3)
        btn_cerrar = driver.find_element_by_xpath('//*[@id="navbarNav"]/ul/li[3]/a')
        btn_cerrar.click()
        cabecera_login = driver.find_element_by_xpath('//*[@id="main"]/app-login/section/div/div/app-form-login/div/div')
        time.sleep(2)
        self.assertEqual(cabecera_login.text, 'JavaShot')
        take_screenshot(self.driver, 'cerrar_sesion')

    def login(self, driver):
        time.sleep(2)
        input_user = driver.find_element_by_name('email')
        password = driver.find_element_by_name('password')
        button_login = driver.find_element_by_id('btn-login')

        input_user.send_keys('javier@ejemplo.com')
        password.send_keys('123456')
        button_login.click()
        time.sleep(2)
        take_screenshot(self.driver, 'login')

        tituloHome = driver.find_element_by_tag_name('h1')
        self.assertEqual('Tomate un Shot de ¡Java!', tituloHome.text)

    def puntos(self, driver):
        time.sleep(3)
        driver.find_element_by_xpath('//*[@id="navbarNav"]/ul/li[2]/a').click()
        tituloPuntos = driver.find_element_by_tag_name('h2')
        self.assertEqual('Mejores lugares', tituloPuntos.text)
        take_screenshot(self.driver, 'puntos')
        time.sleep(2)

    def tearDown(self) -> None:
        self.driver.quit()


if __name__ == "__main__":
    unittest.main(testRunner=HTMLTestRunner(output="hello_world", report_name="report_hello_world"))
