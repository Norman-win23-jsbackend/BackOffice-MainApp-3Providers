
const formErrorHandler = (element, validationResult) => {
    // Get the name of the input element directly from the element parameter
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

    if (validationResult) {
        element.classList.remove('input-validation-error');
        spanElement.classList.remove('Field-validation-error');
        spanElement.classList.add('Field-validation-valid');
        spanElement.innerHTML = '';
    } else {
        element.classList.add('input-validation-error');
        spanElement.classList.add('Field-validation-error');
        spanElement.classList.remove('Field-validation-valid');
        spanElement.innerHTML = element.dataset.valRequired;
    }
}





const textValidator = (element, minLength = 2) => {
    if (element.value.length >= minLength) {
        formErrorHandler(element, true);
    } else {
        formErrorHandler(element, false);
    }
}



const emailValidator = (element) => {
    const regEx = /^[^@\s]+@[^@\s]+\.[^@\s]{2,}$/;
    formErrorHandler(element, regEx.test(element.value));
}


const passwordValidator = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        const confirmPasswordElement = document.querySelector(`[name="${element.dataset.valEqualtoOther.replace('*', 'Form')}"]`);
        formErrorHandler(element, compareValidator(element.value, confirmPasswordElement.value));
    } else {
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/;
        formErrorHandler(element, regEx.test(element.value));
    }
}

const compareValidator = (value, compareValue) => {
    return value === compareValue;
}


const checkboxValidator = (element) => {
    if (element.checked)
        formErrorHandler(element, true)

    formErrorHandler(element, false)
}




let forms = document.querySelectorAll('form');

let inputs = forms[0].querySelectorAll('input');

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {

                switch (e.target.type) {

                    case 'text':
                        textValidator(e.target);
                        break;

                    case 'email':
                        emailValidator(e.target);
                        break;

                    case 'password':
                        passwordValidator(e.target);

                }
            })
        }
    }
})
